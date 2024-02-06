using Application.Constants;
using Application.Exceptions;
using Application.Features.Roles.Commands;
using Application.Interfaces.Repositories;
using Application.Wrappers.Abstract;
using Application.Wrappers.Concrete;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.IO;
using System.Text;

namespace Application.Features.Users.Commands
{
    public class ImportExcelCommand : IRequest<IResponse>
    {
        public IFormFile formfile { get; set; }

        public class ImportExcelCommandHandler : IRequestHandler<ImportExcelCommand, IResponse>
        {
            private readonly IUserRepository _userRepository;
            private readonly IUnitOfWork _unitOfWork;

            public ImportExcelCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
            {
                _userRepository = userRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task<IResponse> Handle(ImportExcelCommand request, CancellationToken cancellationToken)
            {
                if (request.formfile == null || request.formfile.Length <= 0)
                {
                    throw new ApiException(404, Messages.FileEmpty);
                }

                if (!Path.GetExtension(request.formfile.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
                {
                    throw new ApiException(400, Messages.NotSupportFile);
                }

                var list = new List<User>();
                using (var stream = new MemoryStream())
                {
                    request.formfile.CopyTo(stream);
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    using (var package = new ExcelPackage(stream))
                    {

                        ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                        var rowcount = worksheet.Dimension.Rows;
                        var colcount = worksheet.Dimension.Columns;

                        for (int row = 2; row < rowcount; row++)
                        {
                            list.Add(new User
                            {
                                FirstName = worksheet.Cells[row, 1].Value.ToString().Trim(),
                                UserName = worksheet.Cells[row, 2].Value.ToString().Trim(),
                                LastName = worksheet.Cells[row, 3].Value.ToString().Trim(),
                                Email = worksheet.Cells[row, 4].Value.ToString().Trim(),
                                PasswordHash = Encoding.UTF8.GetBytes(worksheet.Cells[row, 5].Value?.ToString()?.Trim()),
                                PasswordSalt = Encoding.UTF8.GetBytes(worksheet.Cells[row, 6].Value?.ToString()?.Trim()),
                                TenantId = int.Parse(worksheet.Cells[row, 7].Value.ToString().Trim()),
                                EmployeeCode = worksheet.Cells[row, 8].Value.ToString().Trim(),
                                PhoneNumber = worksheet.Cells[row, 8].Value.ToString().Trim(),

                            }) ;
                        }
                    }
                }
                _userRepository.AddRange(list);
                await _unitOfWork.SaveChangesAsync();
                
                return new SuccessResponse(200, Messages.ImportExcelSuccessfully);
            }
        }
    }

}

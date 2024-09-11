using MediatR;
using Microsoft.AspNetCore.Http;
using SednaReservationAPI.Application.Abstractions;
using SednaReservationAPI.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SednaReservationAPI.Application.Features.Commands.Hotel.UpdateHotel
{
    public class UpdateHotelCommandHandler : IRequestHandler<UpdateHotelCommandRequest, UpdateHotelCommandResponse>
    {
        readonly IHotelReadRepository _hotelReadRepository;
        readonly IHotelWriteRepository _hotelWriteRepository;
        readonly IFileService _fileService;
        readonly IHttpContextAccessor _httpContextAccessor;

        public UpdateHotelCommandHandler(IHotelReadRepository hotelReadRepository, IHotelWriteRepository hotelWriteRepository, IFileService fileService, IHttpContextAccessor httpContextAccessor)
        {
            _hotelReadRepository = hotelReadRepository;
            _hotelWriteRepository = hotelWriteRepository;
            _fileService = fileService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<UpdateHotelCommandResponse> Handle(UpdateHotelCommandRequest request, CancellationToken cancellationToken)
        {
          

            Domain.Entities.Hotel hotel = await _hotelReadRepository.GetByIdAsync(request.Id);
            if (request.ImageFile != null)
            {
                
                var fileResult = _fileService.SaveImage(request.ImageFile, 2400, 1600);

             
                var existingImageUrl = hotel.ImageUrl;

                if (fileResult.Item1 == 1 && !string.IsNullOrEmpty(fileResult.Item2))
                {
                    var req = _httpContextAccessor.HttpContext.Request;
                    var baseUrl = $"{req.Scheme}://{req.Host}";
                    // Tam URL'yi oluşturuyoruz
                    request.ImageUrl = Path.Combine(baseUrl, "res", fileResult.Item2).Replace("\\", "/");
                    hotel.ImageUrl = request.ImageUrl;

                    // Eski resmi sil
                    if (!string.IsNullOrEmpty(existingImageUrl))
                    {
                        // Tam URL'den dosya adını çıkarıyoruz
                        var oldImageFileName = Path.GetFileName(existingImageUrl);

                        // Dosya adını silme metoduna gönderiyoruz
                        _fileService.DeleteImage(oldImageFileName);
                            
                    }
                }
            }


            hotel.Name = request.Name;
            hotel.Address = request.Address;
            hotel.Phone = request.Phone;
            hotel.StarRating = request.StarRating;
            hotel.Star = request.Star;
            hotel.Email = request.Email;
            hotel.UpdatedDate = DateTime.UtcNow;
            try
            {
                await _hotelWriteRepository.SaveAsync();

                return new UpdateHotelCommandResponse{ 
                    Success = true,
                Message = "Oteliniz başarıyla güncellendi"};
            }
            catch (Exception err)
            {

                return new UpdateHotelCommandResponse
                {
                    Success = false,
                    Message = "Oteliniz güncellenirken bir hata oluştu.." + err.Message
                };
            }
       

       
        }
    }
}

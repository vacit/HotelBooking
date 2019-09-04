using BookingSystem.Core;
using BookingSystem.Core.Extensions;
using BookingSystem.Data.Context;
using BookingSystem.Domain.Entity;
using BookingSystem.Domain.WebUI;
using BookingSystem.Domain.WebUI.Filters;
using BookingSystem.Domain.WebUI.Hotel;
using BookingSystem.Service.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace BookingSystem.Service.Services
{
    public class HotelService : ServiceBase
    {
        public ServiceResultModel<HotelVM> GetHotel(int id)
        {
            if (id <= 0)
                return null;
            HotelVM currentItem = null;
            using (EFBookingContext context = new EFBookingContext())
            {
                currentItem = context.Hotels.FirstOrDefault(p => p.Id == id).MapProperties<HotelVM>();
            }

            return ServiceResultModel<HotelVM>.OK(currentItem);
        }

        public ServiceResultModel<List<HotelVM>> GetAllHotels(HotelFilter filter)
        {
            List<HotelVM> resultList = new List<HotelVM>();

            using (EFBookingContext context = new EFBookingContext())
            {
                IQueryable<Hotel> HotelList = context.Hotels;

                if (filter.Name.IsNotNull())
                    HotelList = HotelList.Where(p => p.Name.Contains(filter.Name));

                //if (filter.HotelTypeId.HasValue)
                //    HotelList = HotelList.Where(p => p.HotelTypeId == filter.HotelTypeId);

                HotelList.ToList().ForEach(p =>
                {
                    resultList.Add(p.MapProperties<HotelVM>());
                });

                return ServiceResultModel<List<HotelVM>>.OK(resultList);
            }
        }

        public ServiceResultModel<HotelVM> SaveHotel(HotelVM model)
        {
            using (EFBookingContext context = new EFBookingContext())
            {
                bool isAlreadyExists = context.Hotels.Any(p => p.Title == model.Title);
                if (isAlreadyExists)
                    return new ServiceResultModel<HotelVM>
                    {
                        Code = ServiceResultCode.Duplicate,
                        Data = null,
                        ResultType = OperationResultType.Warn,
                        Message = "This record already exists"
                    };

                var recordItem = context.Hotels.Add(model.MapProperties<Hotel>());
                context.SaveChanges();

                return ServiceResultModel<HotelVM>.OK(recordItem.MapProperties<HotelVM>());
            }
        }

        public ServiceResultModel<HotelVM> UpdateHotel(HotelVM model)
        {
            using (EFBookingContext context = new EFBookingContext())
            {
                var currentItem = context.Hotels.FirstOrDefault(p => p.Id == model.Id);
                if (currentItem != null)
                {
                    
                    if (context.Hotels.Any(p => p.Id != model.Id && p.Title.Equals(model.Title)))
                    {
                        return new ServiceResultModel<HotelVM>
                        {
                            Code = ServiceResultCode.Duplicate,
                            Data = currentItem.MapProperties<HotelVM>(),
                            ResultType = OperationResultType.Warn,
                            Message = "This title using other records "
                        };
                    }
                    currentItem.Title = model.Title;
                    currentItem.HotelTypeId = model.HotelTypeId;

                    context.Entry<Hotel>(currentItem).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();
                }

                return ServiceResultModel<HotelVM>.OK(currentItem.MapProperties<HotelVM>());
            }
        }

        public ServiceResultModel<HotelVM> DeleteHotel(int id)
        {
            using (EFBookingContext context = new EFBookingContext())
            {
                var deleteItem = context.Hotels.FirstOrDefault(p => p.Id == id);
                context.Hotels.Remove(deleteItem);
                context.SaveChanges();

                return ServiceResultModel<HotelVM>.OK(deleteItem.MapProperties<HotelVM>());
                /*
                 veya bu şeklide de yazabilirsiniz.
                 EF Tracking sistemi ile çalışır. 2. kodda tracking 'e deleteıtem kaydının Hotel tablosunda deleted olarak Track'lendiğini bildiriyoruz.
                 sonrasında commit 'de ilgili kayıt silinir.

                var deleteItem = context.Hotels.FirstOrDefault(p => p.Id == id);
                context.Entry<Hotel>(deleteItem).State = System.Data.Entity.EntityState.Deleted;
                context.SaveChanges();

                 */
            }
        }
    }
}
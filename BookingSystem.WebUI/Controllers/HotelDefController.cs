using BookingSystem.Domain.WebUI;
using BookingSystem.Domain.WebUI.Filters;
using BookingSystem.Domain.WebUI.Hotel;
using BookingSystem.Service.Services;
using BookingSystem.WebUI.Models.DataTableRequest;
using BookingSystem.WebUI.Models.DataTableResponse;
using BookingSystem.WebUI.Models.Response;
using System.Linq;
using System.Web.Mvc;

namespace BookingSystem.WebUI.Controllers
{
    [Authorize]
    public class HotelDefController : ControllerBase
    {
        private readonly HotelService _hotelService;

        public HotelDefController()
        {
            _hotelService = new HotelService();
        }

        #region HotelsMethods

        #region List-Hotel

        /// <summary>
        /// HotelsList Action
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult HotelList()
        {
            return View();
        }

        /// <summary>
        /// HotelList Action Grid Fill Method
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonResult GetHotelList(DataTableRequest<HotelFilter> model)
        {
            var page = model.start;
            var rowsPerPage = model.length;

            var filteredData = _hotelService.GetAllHotels(model.FilterRequest);
            var gridPageRecord = filteredData.Data.Skip(page).Take(rowsPerPage).ToList();

            DataTablesResponse tableResult = new DataTablesResponse(model.draw, gridPageRecord, filteredData.Data.Count, filteredData.Data.Count);

            return Json(tableResult, JsonRequestBehavior.AllowGet);
        }

        #endregion List-Hotel

        #region AddEdit-Hotel

        [HttpGet]
        public ActionResult HotelAdd()
        {
            return View(new HotelVM());
        }

        [HttpGet]
        public ActionResult HotelEdit(int id)
        {
            var model = _hotelService.GetHotel(id);
            if (model == null)
                RedirectToAction(nameof(HotelList));

            return View(model.Data);
        }

        [HttpPost]
        public JsonResult SaveHotel(HotelVM model)
        {
            if (!ModelState.IsValid)
                return base.JSonModelStateHandle();

            ServiceResultModel<HotelVM> serviceResult = _hotelService.SaveHotel(model);

            if (!serviceResult.IsSuccess)
            {
                base.UIResponse = new UIResponse
                {
                    Message = string.Format("Operation Is Not Completed, {0}", serviceResult.Message),
                    ResultType = serviceResult.ResultType,
                    Data = serviceResult.Data
                };
            }
            else
            {
                base.UIResponse = new UIResponse
                {
                    Data = serviceResult.Data,
                    ResultType = serviceResult.ResultType,
                    Message = "Success"
                };
            }

            return Json(base.UIResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteHotel(int id)
        {
            if (id <= 0)
                return Json(base.UIResponse = new UIResponse
                {
                    ResultType = Core.OperationResultType.Error,
                    Message = string.Format("id is not valid, this Id = {0}", id)
                }, JsonRequestBehavior.AllowGet);

            ServiceResultModel<HotelVM> serviceResult = _hotelService.DeleteHotel(id);
            return Json(base.UIResponse = new UIResponse
            {
                ResultType = serviceResult.ResultType,
                Data = serviceResult.Data,
                Message = serviceResult.ResultType == Core.OperationResultType.Success ? "Record Deleted Successfully" : string.Format("Warning.. {0}", serviceResult.Message)
            });
        }

        [HttpPost]
        public JsonResult UpdateHotel(HotelVM model)
        {
            if (model.Id <= 0)
                RedirectToAction(nameof(HotelList)); // ErrorHandle eklenecek

            if (!ModelState.IsValid)
                return base.JSonModelStateHandle();

            ServiceResultModel<HotelVM> serviceResult = _hotelService.UpdateHotel(model);

            if (!serviceResult.IsSuccess)
            {
                base.UIResponse = new UIResponse
                {
                    Message = string.Format("Operation Is Not Completed, {0}", serviceResult.Message),
                    ResultType = serviceResult.ResultType,
                    Data = serviceResult.Data
                };
            }
            else
            {
                base.UIResponse = new UIResponse
                {
                    Data = serviceResult.Data,
                    ResultType = serviceResult.ResultType,
                    Message = "Success"
                };
            }

            return Json(base.UIResponse, JsonRequestBehavior.AllowGet);
        }

        #endregion AddEdit-Hotel

        #endregion HotelsMethods


    }
}
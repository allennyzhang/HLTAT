using HLTAT.Business.Model;
using HLTAT.Business.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static HLTAT.Utilities.Common.Constant;

namespace HLTAT.API.Controllers
{
    [Route("api/prod")]
    public class ProductController : BaseController
    {
        private readonly IProductService _prodSvc;
        public ProductController(IProductService prodSvc)
        {
            _prodSvc = prodSvc;
        }


        [HttpGet]
        [Route("default")]
        public IActionResult Default()
        {
            try
            {
                return Json(new ResponseModel()
                {
                    Status = RespStatus.Success,
                    Data = $"Helix Leisure Technical Assesment Test. Kindly use postman or anyother API testing tools to test getproduct and putproduct API."
                });

            }
            catch (Exception ex)
            {
                return Json(new ResponseModel() { Status = RespStatus.Failure, Data = ex?.InnerException?.Message ?? ex?.Message });
            }
        }


        [HttpPost]
        [Route("getproduct")]
        [ResponseCache(Duration = 60)]
        public async Task<IActionResult> GetProduct([FromBody] ProductsModel prod)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(new ResponseModel()
                    {
                        Status = RespStatus.Failure,
                        Data = "Model is invalid : " + string.Join("<br>", ModelState.SelectMany(x => x.Value.Errors.Select(z => z.Exception?.Message ?? z.ErrorMessage)))
                    });
                }

                if (prod == null)
                {
                    return Json(new ResponseModel() { Status = RespStatus.Failure, Data = $"[FromBody] Product is null" });
                }

                var existProd = await _prodSvc.GetProduct(prod.ID, prod.Timestamp, prod.Products.FirstOrDefault().ID);

                if (existProd == null)
                {
                    return Json(new ResponseModel() { Status = RespStatus.Failure, Data = $"Product [{prod.ID}] is not exist" });
                }

                return Json(existProd);
            }
            catch (Exception ex)
            {
                return Json(new ResponseModel() { Status = RespStatus.Failure, Data = ex?.InnerException?.Message ?? ex?.Message });
            }
        }



        [HttpPost]
        [Route("putproduct")]
        public async Task<IActionResult> PostProduct([FromBody] ProductsModel prod)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(new ResponseModel()
                    {
                        Status = RespStatus.Failure,
                        Data = "Model is invalid : " + string.Join("<br>", ModelState.SelectMany(x => x.Value.Errors.Select(z => z.Exception?.Message ?? z.ErrorMessage)))
                    });
                }

                var respMsg = string.Empty;
                var prodObj = prod.Products.FirstOrDefault();
                if (prodObj.Quantity < 0 || prodObj.Quantity > int.MaxValue)
                    respMsg = $"Model is invalid : {prodObj.Quantity} is invalid number for Quantity; ";

                if (prodObj.Sale_Amount < 0 || prodObj.Sale_Amount > double.MaxValue)
                    respMsg += $"{prodObj.Sale_Amount} is invalid number for SaleAmount; ";

                if (!string.IsNullOrWhiteSpace(respMsg))
                {
                    return Json(new ResponseModel() { Status = RespStatus.Failure, Data = respMsg });
                }

                if (prod == null)
                {
                    return Json(new ResponseModel() { Status = RespStatus.Failure, Data = $"[FromBody] Product is null" });
                }

                _prodSvc.AddOrUpdateProduct(prod);
                await _prodSvc.SaveChangesAsync();

                var existProd = await _prodSvc.GetProduct(prod.ID, prod.Timestamp, prod.Products.FirstOrDefault().ID);

                return Json(new ResponseModel()
                {
                    Status = RespStatus.Success,
                    Data = new
                    {
                        existProd.ID,
                        existProd.Timestamp,
                        Products = new List<object> { new { existProd.Products.FirstOrDefault().ID } }
                    }
                });
            }
            catch (Exception ex)
            {
                return Json(new ResponseModel() { Status = RespStatus.Failure, Data = ex?.InnerException?.Message ?? ex?.Message });
            }
        }

    }
}
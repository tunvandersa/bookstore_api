using Microsoft.AspNetCore.Mvc;
using BusinessObjects.DTO;
using DataAccess.Interface;
using Repositories.Interface;

namespace book_store_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SachController : ControllerBase
    {
        private readonly ISachBusiness _sachBusiness;
        public SachController(ISachBusiness sachBusiness)
        {
            _sachBusiness = sachBusiness;
        }
        [HttpGet]
        [Route("List")]
        public async Task<IActionResult> GetListAsync()
        {
            var sach = await _sachBusiness.GetAllAsync();
            return Ok(sach);
        }
        [HttpGet]
        [Route("TheLoai")]
        public async Task<IActionResult> GetTheLoaiAsyn()
        {
            var theloai = await _sachBusiness.GetTLAsync();
            return Ok(theloai);
        }
        [HttpGet]
        [Route("NhaXuatBan")]
        public async Task<IActionResult> GetNXBAsyn()
        {
            var nxb = await _sachBusiness.GetNXBAsync();
            return Ok(nxb);
        }
        [HttpGet]
        [Route("TacGia")]
        public async Task<IActionResult> GetTGAsyn()
        {
            var tacgia = await _sachBusiness.GetTacGiaAsync();
            return Ok(tacgia);
        }
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateAsync([FromBody] SachDto model)
        {

            var response = await _sachBusiness.CreateAsync(model);
            return Ok(response);
        }
        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var sach = await _sachBusiness.GetByIdAsync(id);
            if (sach == null)
            {
                return NotFound();
            }
            var response = await _sachBusiness.DeleteAsync(id);
            return Ok(response);
        }
        [HttpPut]
        [Route("Update /{id}")]
        public async Task<IActionResult> UpdateAsync([FromBody] SachDto model) 
        {
            var sach = await _sachBusiness.GetByIdAsync(model.MaSach);
            if (sach == null)
            {
                return NotFound();
            }
            var response = await _sachBusiness.UpdateAsync(model);
            return Ok(response);
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var sach = await _sachBusiness.GetByIdAsync(id);
            return Ok(sach);
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNetEFCore.Controller
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StudentController : ControllerBase
    {

        private StudentContext _dbContext;

        public StudentController(StudentContext dbContext)
        {
            this._dbContext = dbContext;
        }

        /// <summary>
        /// 创建学生
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult CreateStudent(Student student)
        {

            this._dbContext.Add(student);

            return Content("1");
        }
    }
}

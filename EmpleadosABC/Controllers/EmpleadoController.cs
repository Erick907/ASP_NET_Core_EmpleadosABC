using EmpleadosABC.Models;
using EmpleadosABC.ModelsRequests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmpleadosABC.Controllers
{
    [ApiController, Route("api/empleados")]
    public class EmpleadoController : Controller
    {
        private EmpleadosABCContext _context;

        public EmpleadoController(EmpleadosABCContext context)
        {
            _context = context;
        }

        // Get all employees
        [HttpGet]
        public JsonResult Index()
        {
            return Json(_context.Empleados
                .Include(e => e.Estatus)
                .Select(e => new 
                {
                    empleadoId = e.EmpleadoId,
                    nombre = e.Nombre,
                    apellidoPaterno = e.ApellidoPaterno,
                    apellidoMaterno = e.ApellidoMaterno,
                    fechaNacimiento = e.FechaNacimiento,
                    estatusId = e.EstatusId,
                    estatus = e.Estatus.Descripción

                })
                .ToArray());
        }

        // Find employee by id
        [HttpGet, Route("find")]
        public JsonResult Find(int? empleadoId)
        {
            object? resp = null;

            if (empleadoId == null)
            {
                resp = new { empleadoId = "The [empleadoId] parameter is required!" };
                Response.StatusCode = 400;
            }
            else
            {
                resp = _context.Empleados
                    .Include(e => e.Estatus)
                    .Where(e => e.EmpleadoId == empleadoId)
                    .Select(e => new
                    {
                        empleadoId = e.EmpleadoId,
                        nombre = e.Nombre,
                        apellidoPaterno = e.ApellidoPaterno,
                        apellidoMaterno = e.ApellidoMaterno,
                        fechaNacimiento = e.FechaNacimiento,
                        estatusId = e.EstatusId,
                        estatus = e.Estatus.Descripción

                    })
                    .FirstOrDefault();

                if (resp == null)
                {
                    resp = new { employee_not_found = "The employee with the received [empleadoId] does not exist!" };
                    Response.StatusCode = 400;
                }
            }

            return Json(resp);
        }

        // Create employee
        [HttpPost, Route("create")]
        public JsonResult Create(Employee employee)
        {
            var newEmployee = new Empleado();
            newEmployee.Nombre = employee.Name;
            newEmployee.ApellidoPaterno = employee.LastName1;
            newEmployee.ApellidoMaterno = employee.LastName2;
            newEmployee.FechaNacimiento = Convert.ToDateTime(employee.BirthDate);
            newEmployee.EstatusId = (int)employee.EstatusId;

            _context.Empleados.Add(newEmployee);
            _context.SaveChanges();

            var newEmployeeAux = _context.Empleados
                .Include(e => e.Estatus)
                .Where(e => e.EmpleadoId == newEmployee.EmpleadoId)
                .Select(e => new
                {
                    empleadoId = e.EmpleadoId,
                    nombre = e.Nombre,
                    apellidoPaterno = e.ApellidoPaterno,
                    apellidoMaterno = e.ApellidoMaterno,
                    fechaNacimiento = e.FechaNacimiento,
                    estatusId = e.EstatusId,
                    estatus = e.Estatus.Descripción

                })
                .FirstOrDefault();

            return Json(new { message = "Success", new_employee = newEmployeeAux });
        }

        // Update employee
        [HttpPut, Route("update")]
        public JsonResult Update(Employee employee)
        {
            object? resp = null;

            var employeeId = _context.Empleados
                .Include(e => e.Estatus)
                .Where(e => e.EmpleadoId == employee.EmployeeId)
                .Select(e => new { e.EmpleadoId })
                .FirstOrDefault();

            if (employeeId == null)
            {
                resp = new { employee_not_found = "The employee with the received [EmployeeId] does not exist!" };
                Response.StatusCode = 400;
            }
            else
            {
                var employeeToUpdate = new Empleado();
                employeeToUpdate.EmpleadoId = (int)employee.EmployeeId;
                employeeToUpdate.Nombre = employee.Name;
                employeeToUpdate.ApellidoPaterno = employee.LastName1;
                employeeToUpdate.ApellidoMaterno = employee.LastName2;
                employeeToUpdate.FechaNacimiento = Convert.ToDateTime(employee.BirthDate);
                employeeToUpdate.EstatusId = (int)employee.EstatusId;

                _context.Empleados.Update(employeeToUpdate);
                _context.SaveChanges();

                var updatedEmployee = _context.Empleados
                    .Include(e => e.Estatus)
                    .Where(e => e.EmpleadoId == employeeToUpdate.EmpleadoId)
                    .Select(e => new
                    {
                        empleadoId = e.EmpleadoId,
                        nombre = e.Nombre,
                        apellidoPaterno = e.ApellidoPaterno,
                        apellidoMaterno = e.ApellidoMaterno,
                        fechaNacimiento = e.FechaNacimiento,
                        estatusId = e.EstatusId,
                        estatus = e.Estatus.Descripción

                    })
                    .FirstOrDefault();

                resp = new { message = "Success", updated_employee = updatedEmployee };
            }

            return Json(resp);
        }

        // Delete employee
        [HttpGet, Route("delete")]
        public JsonResult Delete(int? empleadoId)
        {
            object? resp = null;

            if (empleadoId == null)
            {
                resp = new { empleadoId = "The [empleadoId] parameter is required!" };
                Response.StatusCode = 400;
            }
            else
            {
                var employee = _context.Empleados
                    .Include(e => e.Estatus)
                    .Where(e => e.EmpleadoId == empleadoId)
                    .Select(e => new
                    {
                        empleadoId = e.EmpleadoId,
                        nombre = e.Nombre,
                        apellidoPaterno = e.ApellidoPaterno,
                        apellidoMaterno = e.ApellidoMaterno,
                        fechaNacimiento = e.FechaNacimiento,
                        estatusId = e.EstatusId,
                        estatus = e.Estatus.Descripción

                    })
                    .FirstOrDefault();

                if (employee != null)
                {
                    var employeeToDelete = new Empleado();
                    employeeToDelete.EmpleadoId = (int)empleadoId;
                    _context.Empleados.Remove(employeeToDelete);
                    _context.SaveChanges();
                }

                resp = new { message = "Success" };
            }

            return Json(resp);
        }
    }
}

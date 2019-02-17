using SiteCoreTestWebAPI.Models;
using SiteCoreTestWebAPI.Models.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SiteCoreTestWebAPI.Controllers
{
    [RoutePrefix("api/account")]
    public class AccountController : ApiController
    {        
        [Route("login")]
        [HttpPost]
        public IHttpActionResult Login(UserRequest request)
        {
            var response = new UserResponse();
            try
            {                
                SqlConnection conn = new SqlConnection("Server=tcp:sitecoretest22.database.windows.net,1433;Initial Catalog=SiteCoreTest;Persist Security Info=False;User ID=testjex;Password=aaaa1111?;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
                conn.Open();

                SqlCommand command = new SqlCommand("SELECT u.UserName, u.UserEmail, r.RoleName FROM [User] u(nolock) INNER JOIN RoleAssignment ra(nolock) ON u.UserID=ra.userID INNER JOIN Roles r(nolock) ON ra.RoleID=r.RoleID WHERE u.UserEmail=@email AND u.UserPassword=@password AND u.Active=1", conn);
                command.Parameters.AddWithValue("@email", request.email);
                command.Parameters.AddWithValue("@password", request.password);
                // int result = command.ExecuteNonQuery();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if(reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            UserDto _user = new UserDto()
                            {
                                UserEmail = reader["UserEmail"].ToString(),
                                UserName = reader["UserName"].ToString(),
                                UserRoles = reader["RoleName"].ToString()
                            };
                            //return reader["UserName"].ToString();
                            response.Code = 204;
                            response.Status = 1;
                            response.Message = "SUCCESS";
                            response.AuthorizedUser = _user;
                        }
                    }
                    else
                    {
                        response.Code = 204;
                        response.Status = 1;
                        response.Message = "User not found.";
                        response.AuthorizedUser = null;
                    }                    
                }
                conn.Close();                         
            }
            catch (Exception ex)
            {
                response.Status = 0;
                response.Code = 0;
                response.Message = ex.Message;
            }
            return Ok(response);
        }

        [Route("register")]
        [HttpPost]
        public IHttpActionResult Register(UserRequest request)
        {
            var response = new UserResponse();
            try
            {
                SqlConnection conn = new SqlConnection("Server=tcp:sitecoretest22.database.windows.net,1433;Initial Catalog=SiteCoreTest;Persist Security Info=False;User ID=testjex;Password=aaaa1111?;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
                conn.Open();

                string strSQL = "";
                strSQL += "INSERT INTO [User] VALUES(@email, @password, @username, 1, GETDATE(), 'SiteCoreTestWebAPI', GETDATE(), 'SiteCoreTestWebAPI') ";
                strSQL += "declare @newUserID int SET @newUserID = (SELECT MAX(UserID) FROM [User](nolock)) ";
                strSQL += "INSERT INTO RoleAssignment VALUES(2, @newUserID, 1, GETDATE(), 'SiteCoreTestWebAPI', GETDATE(), 'SiteCoreTestWebAPI') ";
                strSQL += "SELECT u.UserName, u.UserEmail, r.RoleName FROM [User] u(nolock) INNER JOIN RoleAssignment ra(nolock) ON u.UserID=ra.userID INNER JOIN Roles r(nolock) ON ra.RoleID=r.RoleID WHERE u.UserEmail=@email AND u.UserPassword=@password AND u.UserName=@username AND u.Active=1 ";

                SqlCommand command = new SqlCommand(strSQL, conn);
                command.Parameters.AddWithValue("@email", request.email);
                command.Parameters.AddWithValue("@password", request.password);
                command.Parameters.AddWithValue("@username", request.userName);
                //int result = command.ExecuteNonQuery();               
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            UserDto _user = new UserDto()
                            {
                                UserEmail = reader["UserEmail"].ToString(),
                                UserName = reader["UserName"].ToString(),
                                UserRoles = reader["RoleName"].ToString()
                            };
                            //return reader["UserName"].ToString();
                            response.Code = 204;
                            response.Status = 1;
                            response.Message = "SUCCESS";
                            response.AuthorizedUser = _user;
                        }
                    }
                    else
                    {
                        response.Code = 204;
                        response.Status = 1;
                        response.Message = "User not found.";
                        response.AuthorizedUser = null;
                    }
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                response.Status = 0;
                response.Code = 0;
                response.Message = ex.Message;
            }
            return Ok(response);
        }

        [Route("getrole")]
        [HttpPost]
        public IHttpActionResult GetRole(UserRequest request)
        {
            var response = new UserResponse();
            try
            {
                SqlConnection conn = new SqlConnection("Server=tcp:sitecoretest22.database.windows.net,1433;Initial Catalog=SiteCoreTest;Persist Security Info=False;User ID=testjex;Password=aaaa1111?;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
                conn.Open();

                SqlCommand command = new SqlCommand("SELECT u.UserEmail, u.UserName, r.RoleName FROM [User] u(nolock) INNER JOIN RoleAssignment ra(nolock) ON u.UserID = ra.userID INNER JOIN Roles r(nolock) ON ra.RoleID = r.RoleID WHERE u.UserName = @UserName", conn);
                command.Parameters.AddWithValue("@UserName", request.userName);
                // int result = command.ExecuteNonQuery();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {                                                   
                            UserDto _user = new UserDto()
                            {
                                UserEmail = reader["UserEmail"].ToString(),
                                UserName = reader["UserName"].ToString(),
                                UserRoles = reader["RoleName"].ToString()
                            };
                            //return reader["UserName"].ToString();
                            response.Code = 204;
                            response.Status = 1;
                            response.Message = "SUCCESS";
                            response.AuthorizedUser = _user;
                        }
                    }
                    else
                    {
                        response.Code = 204;
                        response.Status = 1;
                        response.Message = "User not found.";
                        response.AuthorizedUser = null;
                    }
                }
                conn.Close();                
            }
            catch (Exception ex)
            {
                response.Status = 0;
                response.Code = 0;
                response.Message = ex.Message;
            }
            return Ok(response);
        }
    }
}

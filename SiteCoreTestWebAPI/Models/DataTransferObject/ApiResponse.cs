using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SiteCoreTestWebAPI.Models.DataTransferObject
{
    public abstract class ApiResponse
    {
        /// <summary>
        /// 1: PASSED
        /// 0: FAILED
        /// </summary>
        [Required]
        public int Status { get; set; } = 0;

        /// <summary>
        /// Conditional
        /// </summary>
        [Required]
        public int Code { get; set; } = 0;

        [Required]
        public string Message { get; set; } = "";
    }
}
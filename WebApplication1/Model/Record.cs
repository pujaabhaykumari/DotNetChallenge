using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Model
{
	public class Record
	{
		
		public string organizationId { get; set; }
		public string deviceId { get; set; }
		public string businessUnitId { get; set; }
		[Required]
		public decimal temperature { get; set; }

		[Required]
		public int HeartRate { get; set; }
	}
}

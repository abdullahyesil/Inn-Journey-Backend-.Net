using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SednaReservationAPI.Application.Abstractions
{
    public interface IFileService
    {
        public Tuple<int, string> SaveImage(IFormFile file, int width, int height);
        public bool DeleteImage(string imageFileName);
    }
}

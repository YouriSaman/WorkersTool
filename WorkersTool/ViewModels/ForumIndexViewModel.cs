using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Models;

namespace WorkersTool.ViewModels
{
    public class ForumIndexViewModel
    {
        public List<Message> Messages { get; set; }
        public Message Message { get; set; }

        public IFormFile Image { get; set; }
    }
}

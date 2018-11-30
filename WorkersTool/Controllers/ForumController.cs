using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Logic;
using Microsoft.AspNetCore.Mvc;
using Models;
using WorkersTool.ViewModels;

namespace WorkersTool.Controllers
{
    public class ForumController : Controller
    {
        private ForumLogic forumLogic = new ForumLogic();

        public IActionResult Index()
        {
            var viewModel = new ForumIndexViewModel();
            viewModel.Messages = forumLogic.GetAllMessages();
            return View(viewModel);
        }

        public IActionResult IndexWithId(Reply reply)
        {
            var viewModel = new ForumIndexViewModel();
            viewModel.Messages = forumLogic.GetAllMessages();
            return Redirect($"{Url.Action("Index", new { ForumIndexViewModel = viewModel })}#" + reply.MessageId);
        }

        public IActionResult AddReply(int messageId)
        {
            var viewModel = new AddReplyViewModel {Reply = new Reply() {MessageId = messageId}};
            return PartialView(viewModel);
        }

        [HttpPost]
        public IActionResult AddReply(AddReplyViewModel viewModel)
        {
            var userId = Convert.ToInt32(Convert.ToString(User.Claims.Where(claim => claim.Type == "Id").Select(claim => claim.Value).SingleOrDefault()));
            var reply = viewModel.Reply;
            reply.UserId = userId;
            
            forumLogic.AddReply(reply);

            return RedirectToAction("IndexWithId", reply);
        }
    }
}
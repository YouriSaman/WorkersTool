using Logic;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Linq;
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

        public IActionResult IndexWithId(int messageId)
        {
            var viewModel = new ForumIndexViewModel();
            viewModel.Messages = forumLogic.GetAllMessages();
            return Redirect($"{Url.Action("Index", new { ForumIndexViewModel = viewModel })}#" + messageId);
        }

        [HttpPost]
        public IActionResult AddMessage(ForumIndexViewModel viewModel)
        {
            var userId = Convert.ToInt32(Convert.ToString(User.Claims.Where(claim => claim.Type == "Id").Select(claim => claim.Value).SingleOrDefault()));
            var message = viewModel.Message;
            message.UserId = userId;

            var messageId = forumLogic.AddMessage(message);

            return RedirectToAction("IndexWithId", new{messageId = messageId});
        }

        public IActionResult AddReply(int messageId)
        {
            var viewModel = new AddReplyViewModel() {Reply = new Reply() {MessageId = messageId}};
            return PartialView(viewModel);
        }

        [HttpPost]
        public IActionResult AddReply(AddReplyViewModel viewModel)
        {
            var userId = Convert.ToInt32(Convert.ToString(User.Claims.Where(claim => claim.Type == "Id").Select(claim => claim.Value).SingleOrDefault()));
            var reply = viewModel.Reply;
            reply.UserId = userId;
            
            forumLogic.AddReply(reply);
            var messageId = reply.MessageId;

            return RedirectToAction("IndexWithId", new {messageId = messageId});
        }

        public IActionResult DeleteMessage(ForumIndexViewModel viewModel)
        {
            var userId = Convert.ToInt32(Convert.ToString(User.Claims.Where(claim => claim.Type == "Id").Select(claim => claim.Value).SingleOrDefault()));
            var messageId = viewModel.MessageId;
            forumLogic.DeleteMessage(messageId, userId);
            return RedirectToAction("Index");
        }

        public IActionResult DeleteReply(ForumIndexViewModel viewModel)
        {
            var userId = Convert.ToInt32(Convert.ToString(User.Claims.Where(claim => claim.Type == "Id").Select(claim => claim.Value).SingleOrDefault()));
            var replyId = viewModel.ReplyId;
            forumLogic.DeleteReply(replyId, userId);
            return RedirectToAction("Index");
        }
    }
}
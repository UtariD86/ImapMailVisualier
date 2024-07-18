﻿using MailTestProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace MailTestProject.ViewComponents
{
    public class ChatViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(dynamic arguments)
        {
            var model = new ChatViewModel();
            model.Emails = arguments.emails != null && arguments.emails.Count > 0 ? arguments.emails : null;
            return View(model);
        }
    }
}

using LinkShortener.Interfaces;

namespace LinkShortener.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public sealed class LinkShortenerController : Controller
	{
		private ILinkShortenerService _linkShortener;
		public LinkShortenerController(ILinkShortenerService linkShortener)
		{
			_linkShortener = linkShortener;
		}

		[HttpGet]
		[Route("GetFullLink")]
		public ActionResult GetFullLink(string token)
		{
			var userUri = _linkShortener.GetUriAndIncreaseCounter(token);
			return new RedirectResult(userUri.FullUri);
		}

		[HttpPost]
		[Route("CreateShortLink")]
		public string CreateShortLink(string fullLink)
		{
			var creator = HttpContext.Request.Cookies.FirstOrDefault(o => o.Key.Equals("__RequestVerificationToken")).Value;
			var userUri = _linkShortener.CreateShortLink(fullLink, creator, HttpContext.Connection.LocalPort.ToString());
			return userUri.ShortUri;
		}

		[HttpGet]
		[Route("GetUserLinks")]
		public IList<string> GetUserLinks()
		{
			var creator = HttpContext.Request.Cookies.FirstOrDefault(o => o.Key.Equals("__RequestVerificationToken")).Value;
			var userUries = _linkShortener.GetAllUserUries(creator);
			return userUries.Select(o => o.ShortUri).ToList();
		}

		[HttpGet]
		[Route("GetAllLinksAndClicks")]
		public IList<Tuple<string, int>> GetAllLinksAndClicks()
		{
			var userUries = _linkShortener.GetAllUries();
			return userUries.Select(o => new Tuple<string,int>(o.ShortUri, o.ClickCounter)).ToList();
		}

		[HttpGet]
		[Route("DeleteAll")]
		public void DeleteAll()
		{
			_linkShortener.DeleteAll();
		}

		[HttpGet]
		[Route("~/q")]
		public IActionResult RedirectToFullLink(string token)
		{
			var uri = _linkShortener.GetUriAndIncreaseCounter(token);
			return new RedirectResult(uri.FullUri);
		}
	}
}

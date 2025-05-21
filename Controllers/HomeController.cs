using Microsoft.AspNetCore.Mvc;

public class HomeController : Controller
{
    private readonly IUserRepository _userRepository;

    public HomeController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var users = await _userRepository.GetAllAsync();

        var model = new CreateUserViewModel
        {
            ExistingUsers = users?.ToList() ?? new List<User>()
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Index(CreateUserViewModel model)
    {
        if (ModelState.IsValid)
        {
            var newUser = new User
            {
                Name = model.Name,
                Tasks = new List<Tasks>()
            };

            await _userRepository.CreateAsync(newUser);
            TempData["SuccessMessage"] = "Usuário criado com sucesso!";
            return RedirectToAction(nameof(Index));
        }

        model.ExistingUsers = (await _userRepository.GetAllAsync())?.ToList() ?? new List<User>();
        return View(model);
    }

}

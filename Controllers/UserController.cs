using Microsoft.AspNetCore.Mvc;

public class UserController : Controller
{
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpGet]
    public async Task<IActionResult> User(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return RedirectToAction("Index", "Home");

        var user = (await _userRepository.GetAllAsync())
            ?.FirstOrDefault(u => u.Name == name);

        if (user == null)
            return NotFound();

        var model = new UserTasksViewModel
        {
            Id  = user.Id,
            Name = user.Name,
            Tasks = user.Tasks
        };

        return View(model);
    }
    [HttpPost]
    public async Task<IActionResult> AdicionarTarefa(Guid UserId, string Title, string Description, DateTime? DateStart, DateTime? DateFinish)
    {
        if (UserId == Guid.Empty || string.IsNullOrWhiteSpace(Title))
            return BadRequest("Dados inválidos.");

        var user = await _userRepository.GetByIdAsync(UserId);
        if (user == null)
            return NotFound();

        var newTask = new Tasks
        {
            Id = Guid.NewGuid(),
            Title = Title,
            Description = Description,
            DateStart = DateStart,
            DateFinish = DateFinish
        };

        user.Tasks.Add(newTask);

        // Atualiza o usuário com a nova tarefa
        await _userRepository.UpdateAsync(user.Id, user);

        return Json(new
        {
            id = newTask.Id,
            title = newTask.Title,
            description = newTask.Description,
            dateStart = newTask.DateStart,
            dateFinish = newTask.DateFinish
        });
    }





}

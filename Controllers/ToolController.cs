using Microsoft.AspNetCore.Mvc;
using ToolHub.InfraStrcuture; // Para ToolViewModel, CreateToolViewModel, AddTopicViewModel
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;
// Não é necessário usar MongoDB.Bson.Serialization.Attributes e MongoDB.Bson aqui
// se Tool e Topics estão em arquivos separados e já têm esses atributos.

// Removidas as definições duplicadas de Tool, Topics, ViewModels e ToolFactory.
// Elas devem estar definidas em seus próprios arquivos (ex: Models/Tool.cs, InfraStrcuture/CreateToolViewModel.cs).

public class ToolController : Controller
{
    private readonly IToolRepository _toolRepository;

    public ToolController(IToolRepository toolRepository)
    {
        _toolRepository = toolRepository;
    }

    // Ação para exibir uma única ferramenta e seus tópicos
    [HttpGet]
    public async Task<IActionResult> Tool(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            // Redireciona para a ação Index (lista de ferramentas) do ToolController
            return RedirectToAction("Index", "Tool");

        var tool = (await _toolRepository.GetAllAsync())
            ?.FirstOrDefault(u => u.Name == name);

        if (tool == null)
            return NotFound();

        var model = new ToolViewModel
        {
            Id = tool.Id,
            Name = tool.Name,
            Topics = tool.Topics
        };

        return View(model);
    }

    // Ação para exibir todas as ferramentas existentes e o formulário de criação
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var allTools = await _toolRepository.GetAllAsync();
        var model = new CreateToolViewModel // Usando CreateToolViewModel para a página principal
        {
            ExistingTools = allTools?.ToList() ?? new List<Tool>() // Popula a lista de ferramentas existentes
        };
        return View(model);
    }

    // AÇÃO CORRIGIDA/ADICIONADA: Processar a criação de uma nova ferramenta
    [HttpPost]
    public async Task<IActionResult> Create(CreateToolViewModel model)
    {
        if (ModelState.IsValid) // Verifica se o modelo é válido (ex: Name não é nulo)
        {
            // Usando ToolFactory para criar a nova ferramenta
            var newTool = ToolFactory.CreateDefault(model.Name);

            // Assumindo que IToolRepository tem um método AddAsync
            // Certifique-se de que a interface IToolRepository em seu projeto
            // (ex: InfraStrcuture/IToolRepository.cs) tenha o método:
            // Task AddAsync(Tool tool);
            await _toolRepository.CreateAsync(newTool);

            // Redireciona para a ação Index (lista de ferramentas) após a criação
            return RedirectToAction("Index");
        }
        // Se o modelo não for válido, recarrega a página com os erros de validação
        // Para que ExistingTools não seja nulo ao retornar a view, popule-o novamente
        model.ExistingTools = (await _toolRepository.GetAllAsync())?.ToList() ?? new List<Tool>();
        return View(model);
    }

    // Ação para adicionar um tópico a uma ferramenta existente (corrigida)
    [HttpPost]
    public async Task<IActionResult> AdicionarTopico(Guid toolId, string Title, string Text)
    {
        if (toolId == Guid.Empty || string.IsNullOrWhiteSpace(Title))
            return BadRequest("Dados inválidos: ID da ferramenta ou Título do tópico ausente.");

        var tool = await _toolRepository.GetByIdAsync(toolId);
        if (tool == null)
            return NotFound("Ferramenta não encontrada.");

        var newTopic = new Topics
        {
            Id = Guid.NewGuid(),
            Title = Title,
            Text = Text
        };

        // Garante que a lista de tópicos não é nula antes de adicionar
        if (tool.Topics == null)
        {
            tool.Topics = new List<Topics>();
        }
        tool.Topics.Add(newTopic);

        // Atualiza a ferramenta com o novo tópico
        await _toolRepository.UpdateAsync(tool.Id, tool);

        return Json(new
        {
            id = newTopic.Id,
            title = newTopic.Title,
            text = newTopic.Text // CORREÇÃO: 'text' em camelCase para consistência com JS
        });
    }
}

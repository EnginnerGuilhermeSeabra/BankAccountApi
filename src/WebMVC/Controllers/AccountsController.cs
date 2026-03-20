using MediatR;
using Microsoft.AspNetCore.Mvc;
using TES.Application.Accounts.Commands.CreateAccount;
using TES.Application.Accounts.Commands.DeleteAccount;
using TES.Application.Accounts.Commands.UpdateAccount;
using TES.Application.Accounts.Queries.GetAccountById;
using TES.Application.Accounts.Queries.GetAllAccounts;
using TES.WebMVC.Models;

namespace TES.WebMVC.Controllers;

/// <summary>
/// Controller responsável pelo CRUD de contas bancárias em MVC.
/// Segue o padrão MVC + CQRS via MediatR.
/// </summary>
[Route("[controller]/[action]")]
public sealed class AccountsController : Controller
{
    private readonly IMediator _mediator;
    private readonly ILogger<AccountsController> _logger;

    public AccountsController(IMediator mediator, ILogger<AccountsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>Lista todas as contas (index).</summary>
    [HttpGet]
    public async Task<IActionResult> Index(CancellationToken ct)
    {
        var accounts = await _mediator.Send(new GetAllAccountsQuery(), ct);
        return View(accounts);
    }

    /// <summary>Exibe detalhes de uma conta específica.</summary>
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Details(Guid id, CancellationToken ct)
    {
        try
        {
            var result = await _mediator.Send(new GetAccountByIdQuery(id), ct);
            return View(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao buscar conta com ID {AccountId}", id);
            TempData["Error"] = "Conta não encontrada.";
            return RedirectToAction(nameof(Index));
        }
    }

    /// <summary>Exibe formulário para criar nova conta.</summary>
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    /// <summary>Processa criação de nova conta.</summary>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateAccountCommand command, CancellationToken ct)
    {
        if (!ModelState.IsValid)
            return View(command);

        try
        {
            var result = await _mediator.Send(command, ct);
            TempData["Success"] = "Conta criada com sucesso!";
            return RedirectToAction(nameof(Details), new { id = result.Id });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao criar conta");
            ModelState.AddModelError("", ex.Message);
            return View(command);
        }
    }

    /// <summary>Exibe formulário para editar conta.</summary>
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Edit(Guid id, CancellationToken ct)
    {
        try
        {
            var account = await _mediator.Send(new GetAccountByIdQuery(id), ct);
            var model = new EditAccountViewModel
            {
                Id = account.Id,
                NomeTitular = account.NomeTitular,
                Status = account.Status
            };
            return View(model);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao carregar formulário de edição");
            TempData["Error"] = "Conta não encontrada.";
            return RedirectToAction(nameof(Index));
        }
    }

    /// <summary>Processa atualização de conta.</summary>
    [HttpPost("{id:guid}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, EditAccountViewModel model, CancellationToken ct)
    {
        if (id != model.Id)
            return BadRequest();

        if (!ModelState.IsValid)
            return View(model);

        try
        {
            var command = new UpdateAccountCommand(model.Id, model.NomeTitular, model.Status);

            await _mediator.Send(command, ct);
            TempData["Success"] = "Conta atualizada com sucesso!";

            return RedirectToAction(nameof(Details), new { id });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao atualizar conta com ID {AccountId}", id);
            ModelState.AddModelError("", ex.Message);
            return View(model);
        }
    }

    /// <summary>Exibe confirmação de exclusão.</summary>
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        try
        {
            var account = await _mediator.Send(new GetAccountByIdQuery(id), ct);
            return View(account);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao carregar confirmação de exclusão");
            TempData["Error"] = "Conta não encontrada.";
            return RedirectToAction(nameof(Index));
        }
    }

    /// <summary>Processa exclusão de conta.</summary>
    [HttpPost("{id:guid}"), ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id, CancellationToken ct)
    {
        try
        {
            await _mediator.Send(new DeleteAccountCommand(id), ct);
            TempData["Success"] = "Conta excluída com sucesso!";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao excluir conta com ID {AccountId}", id);
            TempData["Error"] = ex.Message;
            return RedirectToAction(nameof(Index));
        }
    }
}

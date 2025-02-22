using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppMvc.Data;
using AppMvc.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Authorization;

//Email : teste@email.com - FH-V9!!7JeYvWL5

namespace AppMvc.Controllers
{
    [Authorize]
    [Route("meus-alunos")]
    public class AlunosController : Controller
    {
        private readonly ApplicationDbContext _context;
        public AlunosController(ApplicationDbContext context)
        {
            _context = context;
        }

        



        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
			ViewData["Sucesso"] = "Listagem gerada com sucesso!";
			return _context.Aluno != null ?
                    View(await _context.Aluno.ToListAsync()) :
                    Problem("Entity set 'ApplicationDbContext.Aluno' is null.");
		}

        [Route("detalhes/{id:int}")]
        public async Task<IActionResult> Details(int id)
        {
            if (_context.Aluno == null)
            {
                return NotFound();
            }

            var aluno = await _context.Aluno
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aluno == null)
            {
                return NotFound();
            }
            return View(aluno);
        }




        [Route("novo")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("novo")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,DataNascimento,Email,EmailConfirmacao,Avaliacao,Ativo")] Aluno aluno)
        {
            if (ModelState.IsValid)
            {
                _context.Add(aluno);
                await _context.SaveChangesAsync();

				TempData["Sucesso"] = "Aluno cadastrado com sucesso!";
				return RedirectToAction(nameof(Index));
            }
            return View(aluno);
        }




        [Route("editar/{id:int}")]
        public async Task<IActionResult> Edit(int id)
        {
            var aluno = await _context.Aluno.FindAsync(id);
            if (aluno == null)
            {
                return NotFound();
            }
			return View(aluno);
        }

        [HttpPost("editar/{id:int}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,DataNascimento,Email,Avaliacao,Ativo")] Aluno aluno)
        {
            if (id != aluno.Id)
            {
                return NotFound();
            }

            ModelState.Remove("EmailConfirmacao");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aluno);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlunoExists(aluno.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
				TempData["Sucesso"] = "Aluno editado com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            return View(aluno);
        }




        [Route("excluir/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var aluno = await _context.Aluno
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aluno == null)
            {
                return NotFound();
            }

            return View(aluno);
        }
        
        [HttpPost("excluir/{id:int}")] 
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var aluno = await _context.Aluno.FindAsync(id);
            if (aluno != null)
            {
                _context.Aluno.Remove(aluno);
            }

            await _context.SaveChangesAsync();

            TempData["Sucesso"] = "Aluno excluído com sucesso!";
            return RedirectToAction(nameof(Index));
        }




        private bool AlunoExists(int id)
        {
            return _context.Aluno.Any(e => e.Id == id);
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCBooktopia.Data;
using MVCBooktopia.Models;

namespace MVCBooktopia.Controllers
{
    [Authorize]
    public class ClienteModelsController : Controller
    {
        private readonly MVCBooktopiaContext _context;

        public ClienteModelsController(MVCBooktopiaContext context)
        {
            _context = context;
        }

        // GET: ClienteModels
        public async Task<IActionResult> Index()
        {
              return _context.ClientesModel != null ? 
                          View(await _context.ClientesModel.ToListAsync()) :
                          Problem("Entity set 'MVCBooktopiaContext.ClienteModel'  is null.");
        }

        // GET: ClienteModels/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.ClientesModel == null)
            {
                return NotFound();
            }

            var clienteModel = await _context.ClientesModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clienteModel == null)
            {
                return NotFound();
            }

            return View(clienteModel);
        }

        // GET: ClienteModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ClienteModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ClienteModel clienteModel)
        {
            
            clienteModel.Id = Guid.NewGuid();
            _context.Add(clienteModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: ClienteModels/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.ClientesModel == null)
            {
                return NotFound();
            }

            var clienteModel = await _context.ClientesModel.FindAsync(id);
            if (clienteModel == null)
            {
                return NotFound();
            }
            return View(clienteModel);
        }

        // POST: ClienteModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ClienteModel clienteModel)
        {
            if (id != clienteModel.Id)
            {
                return NotFound();
            }
            try
            {
                _context.Update(clienteModel);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteModelExists(clienteModel.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }


        private bool ClienteModelExists(Guid id)
        {
          return (_context.ClientesModel?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

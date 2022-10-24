using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Services;

public class SnackbarService: ISnackbarService
{
    private readonly WebAPIContext _context;
    public SnackbarService(WebAPIContext context)
    {
        _context = context;
    }

    public async Task<Snackbar?> GetSnackbar(int id)
    {
        return await _context.Snackbar.Where(_ => _.id == id).FirstOrDefaultAsync();
        //return await _context.Snackbar.SingleOrDefaultAsync(_ => _.id == id);
    }

    public async Task<List<Snackbar>> GetSnackbars() => await _context.Snackbar.ToListAsync();
    public async Task createSnackbar(Snackbar snackbar)
    {
        _context.Snackbar.Add(snackbar);
        await _context.SaveChangesAsync();
    }
    public async Task deleteSnackbar(int id)
    {
        var snackbar = _context.Snackbar.SingleOrDefault(_ => _.id == id)!;
        _context.Snackbar.Remove(snackbar);
        await _context.SaveChangesAsync();
    }
    public async Task updateSnackbar(Snackbar snackbar)
    {
        _context.Entry(snackbar).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }
    public bool SnackbarExists(int id)
    {
        return _context.Snackbar.Any(e => e.id == id);
    }
}

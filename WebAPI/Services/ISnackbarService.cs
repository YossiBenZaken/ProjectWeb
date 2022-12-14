namespace WebAPI.Services;
using WebAPI.Models;
public interface ISnackbarService
{
    Task<List<Snackbar>> GetSnackbars();
    Task<Snackbar?> GetSnackbar(int id);
    Task createSnackbar(Snackbar snackbar);
    Task deleteSnackbar(int id);
    Task<Snackbar?> SnackbarExists(int id);
    Task updateSnackbar(Snackbar snackbar);
}

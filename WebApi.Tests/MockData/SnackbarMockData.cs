using WebAPI.Models;

namespace WebApi.Tests.MockData;
public class SnackbarMockData
{
    public static List<Snackbar> GetSnackbars()
    {
        return new List<Snackbar>
        {
            new Snackbar
            {
                id = 1,
                product = "פופקורן קטן",
                price = 22
            },
            new Snackbar
            {
                id = 2,
                product = "פופקורן בינוני",
                price = 24
            },
            new Snackbar
            {
                id = 3,
                product = "פופקורן גדול",
                price = 28
            },
            new Snackbar
            {
                id = 4,
                product = "שתייה קטנה",
                price = 17
            },
            new Snackbar
            {
                id = 5,
                product = "שתייה בינונית",
                price = 20
            },
            new Snackbar
            {
                id = 6,
                product = "שתייה גדולה",
                price = 24
            }
        };
    }
    public static List<Snackbar> GetEmptySnackbars() => new List<Snackbar>();
    public static Snackbar addSnackbar() => new Snackbar { id = 25, price = 22, product = "Test" };
    public static Snackbar updateSnackbar() => new Snackbar { id=25, price = 23, product = "Test2" };
}

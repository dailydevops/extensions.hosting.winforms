namespace NetEvolve.Extensions.Hosting.WinForms;

using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;

public interface IFormularProvider
{
    T GetFormular<T>()
        where T : Form;

    ValueTask<T> GetFormularAsync<T>()
        where T : Form;

    Form GetMainFormular();

    ValueTask<Form> GetMainFormularAsync();

    ValueTask<T> GetScopedFormAsync<T>()
        where T : Form;

    ValueTask<T> GetScopedFormAsync<T>(IServiceScope scope)
        where T : Form;

    T GetScopedForm<T>()
        where T : Form;

    T GetScopedForm<T>(IServiceScope scope)
        where T : Form;
}

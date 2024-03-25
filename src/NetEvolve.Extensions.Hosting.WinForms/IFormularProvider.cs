namespace NetEvolve.Extensions.Hosting.WinForms;

using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Unified access to provide windows forms, which can be used to create and manage forms.
/// </summary>
public interface IFormularProvider
{
    /// <summary>
    /// Gets the formular of the specified type.
    /// </summary>
    /// <typeparam name="T">The specified forms.</typeparam>
    /// <returns>The requested form.</returns>
    T GetFormular<T>()
        where T : Form;

    /// <summary>
    /// Gets the formular of the specified type asynchronously.
    /// </summary>
    /// <typeparam name="T">The specified forms.</typeparam>
    /// <returns>The requested form.</returns>
    ValueTask<T> GetFormularAsync<T>()
        where T : Form;

    /// <summary>
    /// Gets the main formular.
    /// </summary>
    /// <returns>The requested form.</returns>
    Form GetMainFormular();

    /// <summary>
    /// Gets the main formular asynchronously.
    /// </summary>
    /// <returns>The requested form.</returns>
    ValueTask<Form> GetMainFormularAsync();

    /// <summary>
    /// Gets the scoped formular of the specified type.
    /// </summary>
    /// <typeparam name="T">The specified forms.</typeparam>
    /// <returns>The requested form.</returns>
    T GetScopedForm<T>()
        where T : Form;

    /// <summary>
    /// Gets the scoped formular of the specified type.
    /// </summary>
    /// <typeparam name="T">The specified forms.</typeparam>
    /// <param name="scope">The scope.</param>
    /// <returns>The requested form.</returns>
    T GetScopedForm<T>(IServiceScope scope)
        where T : Form;

    /// <summary>
    /// Gets the scoped formular of the specified type asynchronously.
    /// </summary>
    /// <typeparam name="T">The specified forms.</typeparam>
    /// <returns>The requested form.</returns>
    ValueTask<T> GetScopedFormAsync<T>()
        where T : Form;

    /// <summary>
    /// Gets the scoped formular of the specified type asynchronously.
    /// </summary>
    /// <typeparam name="T">The specified forms.</typeparam>
    /// <param name="scope">The scope.</param>
    /// <returns>The requested form.</returns>
    ValueTask<T> GetScopedFormAsync<T>(IServiceScope scope)
        where T : Form;
}

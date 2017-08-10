using System.Windows.Forms;

namespace UiControls
{

    public interface IScale
    {
        double HeightScale { get; }
        double WidthScale { get; }
    }

    public interface ICanResize
    {
        bool CanHeightResize { get; }
        bool CanWidthResize { get; }
    }
}

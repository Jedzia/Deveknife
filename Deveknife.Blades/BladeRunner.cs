namespace Deveknife.Blades
{
    using System.Windows.Forms;

    using Deveknife.Api;

    public abstract class BladeRunner
    {
        protected BladeRunner(IHost host, IBladeUI ui)
        {
            Guard.NotNull(() => ui, ui);
            this.UI = ui;
            Guard.NotNull(() => host, host);
            this.Host = host;
        }

        public IHost Host { get; private set; }

        /// <summary>
        /// Gets the name of the <see cref="IBlade" />.
        /// </summary>
        /// <value>The name.</value>
        public virtual string Name
        {
            get
            {
                return this.UI.BladeName;
            }
        }

        // public abstract UserControl CreateControl();
        public IBladeUI UI { get; private set; }

        /// <summary>
        /// Creates the User-Interface control.
        /// </summary>
        /// <returns>a UserControl with the User-Interface of the <see cref="IBlade" />.</returns>
        public virtual UserControl CreateControl()
        {
            return (UserControl)this.UI;
        }
    }
}
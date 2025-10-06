using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace osu.Common
{
    // won't use this. yet.
    internal class TabManager
    {
        private readonly Dictionary<string, ITab> _tabs = new();
        private ITab? _activeT;

        public ITab? active => _activeT;

        public void AddTab(ITab tab)
        {
            if (!_tabs.ContainsKey(tab.TabName))
                _tabs.Add(tab.TabName, tab);

            _tabs[tab.TabName] = tab;
        }

        public void SelectTab(string name) 
        {
            if (!_tabs.TryGetValue(name, out var newTab))
                throw new KeyNotFoundException($"tab '{name}' doesn't exist!");
            _activeT?.OnDeselect();
            
            _activeT = newTab;

            _activeT.OnSelect();
        }

        public void UpdateActivateTab()
        {
            _activeT?.Insides()
        ; //wtf
        }

        public IEnumerable<ITab> GetTabs() => _tabs.Values;
    }
}

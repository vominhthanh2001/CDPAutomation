using CDPAutomation.Interfaces.Pages;
using CDPAutomation.Models.Page;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDPAutomation.Helpers
{
    internal static class PageObjectManager
    {
        internal static List<PageObjectModel> _pages = [];

        internal static List<PageObjectModel> Pages() => _pages;

        internal static List<IPage> IPages => [.. _pages.Select(p => p.Page)];

        internal static void AddPage(PageObjectModel? page)
        {
            ArgumentNullException.ThrowIfNull(page);

            page.Index = _pages.Count + 1;

            if (page.Active)
            {
                _pages.ForEach(p =>
                {
                    p.Active = false;
                });
            }

            page.Active = true;

            _pages.Add(page);

            CheckTarget();
            FixIndex();
        }

        internal static void RemovePage(PageObjectModel? page)
        {
            ArgumentNullException.ThrowIfNull(page);

            PageObjectModel? pageRemove = _pages.FirstOrDefault(p => p.IdPage == page.IdPage);
            ArgumentNullException.ThrowIfNull(pageRemove);

            _pages.Remove(pageRemove);

            CheckTarget();
            FixIndex();
        }

        internal static void RemovePage(IPage page)
        {
            ArgumentNullException.ThrowIfNull(page);

            PageObjectModel? pageRemove = _pages.FirstOrDefault(p => p.Page == page);
            ArgumentNullException.ThrowIfNull(pageRemove);

            _pages.Remove(pageRemove);

            CheckTarget();
            FixIndex();
        }

        internal static void CheckTarget()
        {
            ArgumentNullException.ThrowIfNull(_pages);

            List<PageObjectModel> pages = [.. _pages.Where(x => x.Active)];
            if (pages.Count == 0) throw new Exception("No active page found.");
            else if (pages.Count > 2) throw new Exception("More than one active page found.");
        }

        internal static void FixIndex()
        {
            ArgumentNullException.ThrowIfNull(_pages);

            for (int i = 0; i < _pages.Count; i++)
            {
                _pages[i].Index = i + 1;
            }
        }
    }
}

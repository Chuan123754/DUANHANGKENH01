using appAPI.IRepository;
using appAPI.Models;

namespace appAPI.Repository
{
    public class AccsessViewsRepon : IAccsessViewsRepon
    {
        APP_DATA_DATN _context;
        public AccsessViewsRepon(APP_DATA_DATN context)
        {

            _context = context;

        }
        public async Task CountViewsAccsess()
        {

            AccessView? accessView = _context.AccessViews.AsEnumerable()
                                                         .Where(ac => ac.AccessDate != null && ac.AccessDate.Date == DateTime.Now.Date)
                                                         .FirstOrDefault();

            bool isNew = false;
            if (accessView == null)
            {
                accessView = new AccessView();
                accessView.AccessDate = DateTime.Now;
                accessView.TotalViews = 0;
                isNew = true;
            }
            accessView.TotalViews = accessView.TotalViews + 1;
            if (isNew)
                _context.AccessViews.Add(accessView);
            await _context.SaveChangesAsync();
        }
    }
}

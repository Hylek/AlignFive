using DC.MessageService;
using DC.ServiceLocator;

namespace Utils
{
    public class Locator : BaseLocator
    {
        // Helper method to make code cleaner.
        public static ITinyMessengerHub EventHub => Find<ITinyMessengerHub>();
    }
}
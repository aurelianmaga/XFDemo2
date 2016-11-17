using System.Threading.Tasks;
using Xamarin.Forms;

namespace LCA.Navigation
{
	public interface IPageNavigator
	{
		void Push(Page page);

		void Pop();

		Page Root { get; }
	}
}


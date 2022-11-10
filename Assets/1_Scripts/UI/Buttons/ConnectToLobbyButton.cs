using Core;

namespace UI.Buttons
{
    public class ConnectToLobbyButton : MyButton
    {
        protected override void OnClickButton()
        {
            AppManager.Instance.ConnectToLobby();
        }
    }
}
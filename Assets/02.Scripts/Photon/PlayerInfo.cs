public class PlayerInfo
{
    public string NickName { get; private set; }
    public string CharacterName { get; private set; }

    public PlayerInfo(string nickName, string characterName)
    {
        this.NickName = nickName;
        this.CharacterName = characterName;
    }
}
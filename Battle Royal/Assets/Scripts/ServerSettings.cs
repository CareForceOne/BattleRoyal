using System.Collections;

public class ServerSettings {

	public string Host { get; set; }
	public int curPlayers { get; set; }
	public int maxPlayers { get; set; }
	public string password { get; set; }

	public ServerSettings(string host, int curPlayers, int maxPlayers, string password) {
		this.Host = host;
		this.curPlayers = curPlayers;
		this.maxPlayers = maxPlayers;
		this.password = password;
	}

	bool isValid() {
		return true;
	}
}

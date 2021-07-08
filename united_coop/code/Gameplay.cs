
using Sandbox;
using Sandbox.UI.Construct;
using System;
using System.IO;
using System.Threading.Tasks;

public partial class Gameplay : Sandbox.Game
{
	public Gameplay()
	{
		if ( IsServer )
		{
			new UnitedHud();
		}
	}
	
	public override void ClientJoined( Client client )
	{
		base.ClientJoined( client );

		var player = new PlayerBase();
		client.Pawn = player;

		player.Respawn();
	}
}

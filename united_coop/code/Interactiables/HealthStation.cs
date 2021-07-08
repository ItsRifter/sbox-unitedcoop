using Sandbox;
using System;

[Library( "uc_healthstation", Title = "Health Station" )]
[Hammer.EntityTool( "Health Station", "Health", "Healing station for players" )]
[Hammer.EditorModel( "models/healthstation.vmdl" )]

partial class HealthStation : InteractiveBase
{
	public virtual string WorldModelPath => "models/healthstation.vmdl";
	
	public virtual string UseSound => "medicalheal";

	public virtual int ChargeAmount { get; set; } = 70;
	
	public override void Spawn()
	{
		base.Spawn();

		SetModel( WorldModelPath );
	}
}

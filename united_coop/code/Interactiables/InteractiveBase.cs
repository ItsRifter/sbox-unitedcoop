using Sandbox;
using System;

partial class InteractiveBase : BaseCarriable, IRespawnableEntity
{
	public virtual string WorldModelPath => "models/healthstation.vmdl";
	
	public virtual string UseSound => "medicalheal";

	public virtual InteractType type => InteractType.Health;
	public virtual int ChargeAmount { get; set; } = 70;
	
	public PickupTrigger PickupTrigger { get; protected set; }
	
	public enum InteractType
	{
		Health,
		Armor,
	}

	public override void Spawn()
	{
		base.Spawn();

		SetModel( WorldModelPath );

		PickupTrigger = new PickupTrigger();
		PickupTrigger.SetParent( this );
		PickupTrigger.Position = Position;
	}
	
	public void OnUse( PlayerBase user )
	{
		Log.Info("Using Interactive" );
		Log.Info(ChargeAmount );
		
		if ( ChargeAmount <= 0 )
			return;
		
		if ( type == InteractType.Health )
		{
			user.Health++;

			PlaySound( UseSound );
			
			ChargeAmount--;
		}
	}
}

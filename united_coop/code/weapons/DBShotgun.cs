using Sandbox;


[Library( "uc_doubleshotgun", Title = "Sawed-Off Shotgun" )]
[Hammer.EditorModel( "weapons/rust_pumpshotgun/rust_pumpshotgun.vmdl" )]
partial class DBShotgun : WeaponBase
{ 
	public override string ViewModelPath => "models/weapons/v_doublebarrel.vmdl";
	public override float PrimaryRate => 1;
	public override float SecondaryRate => 0;
	public override AmmoType AmmoType => AmmoType.Buckshot;
	public override int ClipSize => 2;
	public override float ReloadTime => 0.35f;
	public override int Bucket => 3;

	public override void Spawn()
	{
		base.Spawn();

		SetModel( "models/weapons/doublebarrel.vmdl" );  

		AmmoClip = 2;
	}
	
	public override void AttackPrimary()
	{
		TimeSincePrimaryAttack = -0.5f;
		TimeSinceSecondaryAttack = -0.5f;

		if ( !TakeAmmo( 2 ) )
		{
			DryFire();
			return;
		}
		
		DoubleShootEffects();
		PlaySound( "rust_pumpshotgun.shootdouble" );
		
		for ( int i = 0; i < 20; i++ )
		{
			ShootBullet( 0.2f, 0.3f, 10.0f, 3.0f );
		}
	}

	[ClientRpc]
	protected override void ShootEffects()
	{
		Host.AssertClient();

		Particles.Create( "particles/pistol_muzzleflash.vpcf", EffectEntity, "muzzle" );
		Particles.Create( "particles/pistol_ejectbrass.vpcf", EffectEntity, "ejection_point" );

		ViewModelEntity?.SetAnimBool( "fire", true );

		if ( IsLocalPawn )
		{
			new Sandbox.ScreenShake.Perlin(1.0f, 1.5f, 2.0f);
		}

		CrosshairPanel?.CreateEvent( "fire" );
	}

	[ClientRpc]
	protected virtual void DoubleShootEffects()
	{
		Host.AssertClient();

		Particles.Create( "particles/pistol_muzzleflash.vpcf", EffectEntity, "muzzle" );

		ViewModelEntity?.SetAnimBool( "fire_double", true );
		CrosshairPanel?.CreateEvent( "fire" );

		if ( IsLocalPawn )
		{
			new Sandbox.ScreenShake.Perlin(3.0f, 3.0f, 3.0f);
		}
	}

	public override void OnReloadFinish()
	{
		IsReloading = false;

		TimeSincePrimaryAttack = 0;
		TimeSinceSecondaryAttack = 0;

		if ( AmmoClip >= ClipSize )
			return;

		if ( Owner is PlayerBase player )
		{
			var ammo = player.TakeAmmo( AmmoType, 1 );
			if ( ammo == 0 )
				return;

			AmmoClip += ammo;

			if ( AmmoClip < ClipSize )
			{
				Reload();
			}
			else
			{
				FinishReload();
			}
		}
	}

	[ClientRpc]
	protected virtual void FinishReload()
	{
		ViewModelEntity?.SetAnimBool( "reload_finished", true );
	}

	public override void SimulateAnimator( PawnAnimator anim )
	{
		anim.SetParam( "holdtype", 2 ); // TODO this is shit
		anim.SetParam( "aimat_weight", 1.0f );
	}
}

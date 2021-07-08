using Sandbox;
using System;
using System.Linq;

partial class InventoryManagement : BaseInventory
{


	public InventoryManagement( Player player ) : base ( player )
	{

	}

	public override bool Add( Entity ent, bool makeActive = false )
	{
		var player = Owner as PlayerBase;
		var weapon = ent as WeaponBase;
		var notices = !player.SupressPickupNotices;
		//
		// We don't want to pick up the same weapon twice
		// But we'll take the ammo from it Winky Face
		//
		if ( weapon != null && IsCarryingType( ent.GetType() ) )
		{
			var ammo = weapon.AmmoClip;
			var ammoType = weapon.AmmoType;

			if ( ammo > 0 )
			{
				player.GiveAmmo( ammoType, ammo );

				if ( notices )
				{
					Sound.FromWorld( "dm.pickup_ammo", ent.Position );
					PickupFeed.OnPickup( To.Single( player ), $"+{ammo} {ammoType}" );
				}
			}

			ItemRespawn.Taken( ent );

			ent.Delete();
			return false;
		}

		if ( weapon != null && notices )
		{
			Sound.FromWorld( "dm.pickup_weapon", ent.Position );
			PickupFeed.OnPickup( To.Single( player ), $"{ent.ClassInfo.Title}" ); 
		}


		ItemRespawn.Taken( ent );
		return base.Add( ent, makeActive );
	}

	public bool IsCarryingType( Type t )
	{
		return List.Any( x => x.GetType() == t );
	}
}

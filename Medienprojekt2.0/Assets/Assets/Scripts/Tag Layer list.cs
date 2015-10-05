/*Änderung: ProjectilePoolingSystem ProjectileMask und ObstacleMask löschen! (auch im code nicht vergessen)
 * 			EnemeyPrefab (Hat zweimal EnemyVision, unteres löschen)

------------------------------------------------------------------------------
Player
	Tag: Player
	Layer: Default

	Transform
	SpriteRenderer
		OrderInLayer: 4
	BoxCollider2D
 		Material: NoFriction
	Rigidbody2D
	MovementComponent
		GroundMask: Grounds
		GroundCheck: GroundCheck(Transform)
	AttributeComponent
	HealthSystem
	ProjectilePoolingSystem
-----------------------------------------------------------------------------

Boden (Alles worauf Spieler gehen/springen können soll)
	Tag: Ground
	Layer: Grounds

	BoxCollider2D
		Material: NoFriction
	SpriteRenderer
		OrderInLayer: 0
------------------------------------------------------------------------------

Enemy
	Tag: Enemy
	Layer: Default

	Transform
	SpriteRenderer
		OrderInLayer: 3
	RigidBody2D
		GravityScale: 0
	BoxCollider2D
		Material: NoFriction
		IsTrigger: True
	AttributeComponent
	ProjectilePoolingSystem
	EnemyMovement
		WallMask: Walls
		WallCheck: WallCheck (Transform)
		GroundMask: Grounds
		GroundCheck: FlootCheck (Transform)
	EnemyVision
		VisionCheck: VisionCheck (Transform)
----------------------------------------------------------------------------

MainCamera (Anmerkung: Wenn die Kamera nicht folgt: 1. Nicht heulen 2. Checken ob MaxXandY bzw. MinXandY 
						richtig gesetzt sind (Und nein, alles auf 0 ist nicht richtig))
	Tag: MainCamera
	Layer: Default

	Transform
	Camera
	GUI Layer
	Flare Layer
	Audio Listener
	Rigidbody
	FollowCamera
		Player: Player(Transform)
	Skybox
		Custom Skybox: Default-Skybox
-------------------------------------------------------------------------

Projectile

	Tag: Projectile
	Layer: Default

	Transform
	BoxCollider2D
	SpriteRenderer
			OrderInLayer: 2
	Rigidbody2D
	Projectile

*/

	
	
	
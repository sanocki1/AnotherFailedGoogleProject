using Godot;

public partial class Level : Node2D
{
	private Path2D _path;
	private PackedScene _enemy = (PackedScene)GD.Load("res://Enemy/scenes/enemy.tscn");
	private float _spawnTimer = 0f;
	private string _currentLevelPath;
	[Export] public float spawnTime = 5.1f;

	public override void _Ready()
	{
		_currentLevelPath = GetTree().CurrentScene.SceneFilePath;

		LevelHud levelHud = GetNode<LevelHud>("LevelHud");
		levelHud.PlayerLost += OnPlayerLost;

		_path = GetNode<Path2D>("Path2D");
		SpawnEnemy();
	}

	public override void _Process(double delta)
	{
		_spawnTimer += (float)delta;

		if (_spawnTimer >= spawnTime)
		{
			SpawnEnemy();
		}
	}

	private void SpawnEnemy()
	{
		Enemy newEnemy = (Enemy)_enemy.Instantiate();
		_path.AddChild(newEnemy);
		newEnemy.EnemyEscaped += OnEnemyEscaped;
		_spawnTimer = 0;
	}

	private void OnEnemyEscaped()
	{
		LevelHud levelHud = GetNode<LevelHud>("LevelHud");
		levelHud.ApplyDamage(25);
	}

	private void OnPlayerLost()
	{
		var global = (Global)GetNode("/root/Global");
		global.currentLevelPath = _currentLevelPath;
		GetTree().ChangeSceneToFile("res://MainMenu/scenes/game_over_screen.tscn");

	}
}

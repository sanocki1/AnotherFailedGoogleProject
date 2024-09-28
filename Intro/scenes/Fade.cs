using Godot;
using System;

public partial class Fade : Panel
{
    private float _duration = 0.5f; // Czas trwania fade-in
    private Timer _timer;
    private float _elapsedTime = 0.0f; // Czas upływający w trakcie animacji
    private bool _fadeInStarted = false; // Flaga do śledzenia, czy fade-in został rozpoczęty

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        // Inicjalizuj timer i połącz sygnał timeout
        _timer = GetNode<Timer>("Timer");
        _timer.WaitTime = 35.0f; // Ustaw czas oczekiwania na 35 sekund
        _timer.OneShot = true; // Timer uruchomi się tylko raz
        _timer.Connect("timeout", new Callable(this, nameof(StartFadeIn)));
        _timer.Start(); // Rozpocznij timer
        Modulate = new Color(1, 1, 1, 0); // Ustaw początkowe opacity na 0
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        // Sprawdź, czy fade-in jest w toku
        if (_fadeInStarted)
        {
            if (_elapsedTime < _duration)
            {
                // Zwiększ czas upływający
                _elapsedTime += (float)delta;
                
                // Oblicz nową przezroczystość
                float t = Math.Clamp(_elapsedTime / _duration, 0.0f, 1.0f);
                Modulate = new Color(1, 1, 1, t); // Ustaw opacity
            }
            else
            {
                // Zatrzymaj animację, gdy osiągniesz pełną przezroczystość
                _fadeInStarted = false;
            }
        }
    }

    // Funkcja uruchamiająca fade-in
    public void StartFadeIn()
    {
        _elapsedTime = 0.0f; // Zresetuj czas
        _fadeInStarted = true; // Rozpocznij fade-in
    }
}
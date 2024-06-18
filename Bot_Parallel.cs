using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rocket_bot;

public partial class Bot
{
	public Rocket GetNextMove(Rocket rocket)
	{
		// TODO: распараллелить запуск SearchBestMove
		var (turn, score) = CreateTasks(rocket)
            .MaxBy(task => task.Result.Score)
			.GetAwaiter()
			.GetResult();

		return rocket.Move(turn, level);
	}
	
	public List<Task<(Turn Turn, double Score)>> CreateTasks(Rocket rocket)
	{
		List<Task<(Turn Turn, double Score)>> tasks = new();
		for (int i = 0; i < threadsCount; i++)
		{
			Task<(Turn Turn, double Score)> task = Task.Run(() => SearchBestMove(rocket, new Random(random.Next()), iterationsCount / threadsCount));
			tasks.Add(task);
		}
		Task.WaitAll(tasks.ToArray());
		return tasks;
	}
}
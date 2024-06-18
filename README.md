# Rocket Doodle

When you run this project in VS can see the Doodle flying on a rocket.
His main task - flying over all flags in specific order.

You can control the Doodle with A, D and W keys.
However, in Bot.cs class, simple auto-control AI has already been implemented:
it worked pretty well and accurately from the original project state.

While working on this training task, Channel.cs and Bot_Parallel.cs classes were implemented.

In Channel.cs class all methods were implemented in accordance with their description.
Moreover, that class is thread safe: project doesn't crush even if you use manual doodle control.

Bot_Parallel.cs partial class uses for applying parallel computing path search method.
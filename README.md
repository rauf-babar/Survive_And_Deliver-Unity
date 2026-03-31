# Survive & Deliver: Core Systems Architecture

[![Gameplay Trailer](https://img.shields.io/badge/Watch-Gameplay_Trailer-blue)](Trailer/gameplay.mp4)

## Overview
A modular 3D survival and logistics simulation built in Unity. The core objective requires players to manage vehicle resources, navigate hostile environments, and successfully deliver packages. 

This repository contains the core C# codebase, showcasing the underlying system architecture, state-machine logic, and component-based design used to drive the game, isolated from heavy binary art assets for streamlined code review.

## System Architecture & Engineering Features

### 1. State-Machine Driven AI
* Processes complex pathfinding, target acquisition, and combat transitions in under 16ms to ensure smooth runtime performance during high-density encounters.

### 2. Modular Inventory & Interaction System
* Developed a scalable inventory architecture supporting over 20 unique interactive items.
* Implemented interface-driven interaction logic allowing the player to seamlessly pick up, drop, and utilize randomly spawned map items.
* Built a universal raycast interaction system for environment elements, including dynamic map components and operable doors.

### 3. Dynamic Vehicle & Automated Combat Systems
* Architected a comprehensive vehicle controller managing dynamic states such as health, fuel consumption, and upgrade progressions.
* Programmed automated vehicle defense mechanisms, including an autonomous tracking turret and a front-mounted collision-damage system (blader) for mitigating enemy AI.

### 4. Player Combat & Controller Mechanics
* Integrated a dual-mode combat system supporting both ballistic (firearms) and melee (bat) mechanics.
* Utilized modular component design to separate input handling, animation states, and damage calculation logic.

## Technical Stack
* **Engine:** Unity 3D
* **Language:** C#
* **Core Concepts:** Object-Oriented Programming, Finite State Machines, Interface-Driven Development, Event-Driven Architecture

## Gameplay Loop
Players must dynamically balance resource management (fuel, health) while executing delivery missions. Success requires strategic upgrading of vehicle automated defenses and proficient manual combat to survive intelligent enemy pathfinding.

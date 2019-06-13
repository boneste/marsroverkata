# Notes

**Rover**
- It has a position (x,y), the starting position is given
- It has a direction, the starting direction is given
- Executes commands, each command may change its position or direction
- Together with position and direction the Rover receives at startup the dimensions of the grid of coordinates

**Coordinates**
- It's a set of two numbers to indicate the position of an object on the planet surface

**Direction**
- Possible values: N,S,E,W

**Command**
- Possible values: f,b,l,r
- (f,b) : forward / backward
- (l,r) : turn left / turn right
- turning left / right doesn't change the position, just the direction of the Rover
- moving forward / backward changes the position, not the direction of the Rover

**Obstacle**
- It has a position (x,y)
- The Rover cannot operate a movement to a position occupied by an obstacle
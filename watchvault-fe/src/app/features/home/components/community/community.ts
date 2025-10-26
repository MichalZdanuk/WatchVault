import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';

@Component({
  selector: 'app-community',
  imports: [CommonModule],
  templateUrl: './community.html',
  styleUrl: './community.css',
})
export class Community {
  opinions: Opinion[] = [
    {
      text: 'Love tracking my watchlist here — finally a simple way to keep everything organized!',
      name: 'Anna K.',
      avatar: 'assets/users/anna.jpg',
    },
    {
      text: 'The recommendations are really accurate. Found so many movies I would have missed otherwise.',
      name: 'James R.',
      avatar: 'assets/users/james.jpg',
    },
    {
      text: 'The trending section is my favorite — it always helps me decide what to watch next.',
      name: 'Sophie M.',
      avatar: 'assets/users/sophie.jpg',
    },
    {
      text: 'Clean, fast, and intuitive. I love how everything just works without needing a manual.',
      name: 'David L.',
      avatar: 'assets/users/david.jpg',
    },
    {
      text: 'Love the design and dark mode colors. It feels modern and pleasant to use. The purple accent makes it stand out.',
      name: 'Maya P.',
      avatar: 'assets/users/maya.jpg',
    },
    {
      text: 'Finally an app where I can track movies and shows in one place — huge time saver!',
      name: 'Chris T.',
      avatar: 'assets/users/chris.jpg',
    },
    {
      text: 'Dark mode + purple accent = perfection. Feels like home every time I log in.',
      name: 'Elena G.',
      avatar: 'assets/users/elena.jpg',
    },
    {
      text: 'I discovered some hidden gems thanks to the recommendations. Really impressive.',
      name: 'Mark S.',
      avatar: 'assets/users/mark.jpg',
    },
    {
      text: 'Super clean UI, no clutter, exactly what I was looking for! It feels so refreshing compared to other apps.',
      name: 'Tom W.',
      avatar: 'assets/users/tom.jpg',
    },
    {
      text: 'Community-driven trending is awesome — I always know what people are actually watching.',
      name: 'Rachel L.',
      avatar: 'assets/users/rachel.jpg',
    },
  ];
}

interface Opinion {
  text: string;
  name: string;
  avatar: string;
}

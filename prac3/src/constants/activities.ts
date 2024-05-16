export interface Activity {
  id: string;
  title: string;
  description: string;
  imageUrl: string;
  summary: string;
  explanation: Explanation[];
}

export interface Explanation {
  title: string;
  description: string;
  imageUrl: string;
}

export const ACTIVITIES: Activity[] = [
  {
    id: 'activity-1',
    title: 'Activity 1',
    description: 'Description of Activity 1',
    imageUrl: 'https://via.placeholder.com/150',
    summary: 'Summary of Activity 1',
    explanation: [
      {
        title: 'Explanation 1',
        description: 'Description of Explanation 1',
        imageUrl: 'https://via.placeholder.com/150'
      },
      {
        title: 'Explanation 2',
        description: 'Description of Explanation 2',
        imageUrl: 'https://via.placeholder.com/150'
      }
    ]
  },
  {
    id: 'activity-2',
    title: 'Activity 2',
    description: 'Description of Activity 2',
    imageUrl: 'https://via.placeholder.com/150',
    summary: 'Summary of Activity 2',
    explanation: [
      {
        title: 'Explanation 1',
        description: 'Description of Explanation 1',
        imageUrl: 'https://via.placeholder.com/150'
      },
      {
        title: 'Explanation 2',
        description: 'Description of Explanation 2',
        imageUrl: 'https://via.placeholder.com/150'
      }
    ]
  },
  {
    id: 'activity-3',
    title: 'Activity 3',
    description: 'Description of Activity 3',
    imageUrl: 'https://via.placeholder.com/150',
    summary: 'Summary of Activity 3',
    explanation: [
      {
        title: 'Explanation 1',
        description: 'Description of Explanation 1',
        imageUrl: 'https://via.placeholder.com/150'
      },
      {
        title: 'Explanation 2',
        description: 'Description of Explanation 2',
        imageUrl: 'https://via.placeholder.com/150'
      }
    ]
  }
];

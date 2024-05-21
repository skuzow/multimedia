export interface NavItem {
  url: string;
  label: string;
  text: string;
}

export const NAVITEMS: NavItem[] = [
  {
    url: '/#what-to-expect',
    label: 'what-to-expect',
    text: 'What to expect'
  },
  {
    url: '/#rides-and-shows',
    label: 'rides-and-shows',
    text: 'Rides & Shows'
  },
  {
    url: '/#plan-your-visit',
    label: 'plan-your-visit',
    text: 'Plan your visit'
  },
  {
    url: '/#traveler-photos',
    label: 'traveler-photos',
    text: 'Traveler Photos'
  }
];

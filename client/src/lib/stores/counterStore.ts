import { makeAutoObservable } from "mobx";

export default class CounterStore {
  title = "Counter Store";
  count = 42;
  events: string[] = [`Initial count is ${this.count}`];

  constructor() {
    makeAutoObservable(this);
  }

  increment = (amount = 1) => {
    this.count += amount;
    this.events.push(`Incremented by ${amount} - count is now ${this.count}`);
  };

  decrement = (amount = 1) => {
    this.events.push(`Decremented by ${amount} - count is now ${this.count}`);
    this.count -= amount;
  };

  get eventCount() {
    return this.events.length;
  }
}

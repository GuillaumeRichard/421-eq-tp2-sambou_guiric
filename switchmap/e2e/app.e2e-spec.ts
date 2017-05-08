import { TP2Page } from './app.po';

describe('tp2 App', () => {
  let page: TP2Page;

  beforeEach(() => {
    page = new TP2Page();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});

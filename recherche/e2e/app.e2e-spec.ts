import { RecherchePage } from './app.po';

describe('recherche App', () => {
  let page: RecherchePage;

  beforeEach(() => {
    page = new RecherchePage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});

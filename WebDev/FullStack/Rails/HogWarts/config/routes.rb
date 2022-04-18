Rails.application.routes.draw do
  get 'teachers/index'
  get 'teachers/show'
  resources :houses, only: [:index, :show]

  # get 'houses',     to: 'houses#index', as: 'houses'
  # get 'houses/:id', to: 'houses#show',  as: 'house'

  get 'about' to: houses#about

  # GET / will load the index action of the houses controller.
  root to: 'houses#index'


end
